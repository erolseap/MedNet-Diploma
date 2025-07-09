import { useState, useEffect } from "react";
import { getAccessToken } from "../api/auth";

export default function useAccessToken() {
  const [token, setToken] = useState<string | undefined>(undefined);

  const refresh = () => {
    if (typeof window === "undefined") return;
    const newToken = getAccessToken();
    setToken(newToken);
  };

  useEffect(() => {
    refresh();

    const handleStorage = () => refresh();
    window.addEventListener("storage", handleStorage);
    window.addEventListener("access_token_changed", handleStorage);

    return () => {
      window.removeEventListener("storage", handleStorage);
      window.removeEventListener("access_token_changed", handleStorage);
    };
  }, []);

  return token;
}
