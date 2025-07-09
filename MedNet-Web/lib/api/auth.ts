function getAccessToken(): string | undefined {
  if (typeof window === 'undefined') return undefined;
  return window.localStorage.getItem('access_token') ?? undefined;
}

function saveAccessToken(token: string) {
  if (typeof window === 'undefined') return;
  window.localStorage.setItem('access_token', token);
  window.dispatchEvent(new Event("access_token_changed"));
}

function invalidateAccessToken() {
  if (typeof window === 'undefined') return;
  window.localStorage.removeItem('access_token');
  window.dispatchEvent(new Event("access_token_changed"));
}

async function tryRefreshToken(): Promise<string | null> {
  try {
    const res = await fetch('/api/auth/refresh', {
      method: 'POST',
      credentials: 'include', // ensures refresh token cookie is sent
    });

    if (!res.ok) return null;

    const data = await res.json();
    return data.access_token ?? null;
  } catch {
    return null;
  }
}

export {
  getAccessToken,
  saveAccessToken,
  invalidateAccessToken,
  tryRefreshToken
}
