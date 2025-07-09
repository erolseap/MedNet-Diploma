import { useEffect, useState } from "react";
import { checkAuthorization } from "../api/pub/auth";
import useAccessToken from "./use-access-token";

export default function useAuthorization() {
  const token = useAccessToken();
  const [authorized, setAuthorized] = useState(false);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!token) {
      setAuthorized(false);
      setLoading(false);
      return;
    }

    let cancelled = false;

    const verify = async () => {
      setLoading(true);
      try {
        const result = await checkAuthorization({ token: token });
        if (!cancelled) {
          setAuthorized(result.ok);
        }
      } catch {
        if (!cancelled) {
          setAuthorized(false);
        }
      } finally {
        if (!cancelled) {
          setLoading(false);
        }
      }
    };

    verify();

    return () => {
      cancelled = true;
    };
  }, [token]);

  return [authorized, loading];
}
