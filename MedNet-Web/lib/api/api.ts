import { getAccessToken } from "./auth";

type KnownApiError = 'unauthorized' | 'forbidden' | 'not_found';
type ApiError = KnownApiError | (string & {});

type ApiResult<T = void> =
  T extends void
  ? { error: null; ok: true } | { error: ApiError; ok: false }
  : { data: T; error: null; ok: true } | { data: null; error: ApiError; ok: false };

async function api<TResponse = void>(
  path: string,
  {
    params,
    type = 'GET',
    cache = 0,
    token
  }: {
    params?: Record<string, string|number|object>;
    type?: 'GET' | 'POST' | 'DELETE' | 'PATCH' | 'PUT';
    cache?: number;
    token?: string|null;
  } = {}
): Promise<ApiResult<TResponse>> {
  if (!path.startsWith('/')) path = `/${path}`;

  const isClient = typeof window !== 'undefined';
  const finalToken = token === null ? undefined : (token ?? (isClient ? getAccessToken() : undefined));

  const passJsonBody = params != null && type !== 'GET';

  if (params != null && type === 'GET') {
    const query = new URLSearchParams(
      Object.entries(params).reduce((acc, [k, v]) => {
        acc[k] = String(v);
        return acc;
      }, {} as Record<string, string>)
    );
    path += `?${query.toString()}`;
  }

  const baseUrl = process.env.NEXT_PUBLIC_API_URL ?? 'http://localhost:5000';

  try {
    const res = await fetch(`${baseUrl}${path}`, {
      method: type,
      headers: {
        Accept: "application/json",
        ...(passJsonBody && { "Content-Type": "application/json" }),
        ...(finalToken && { Authorization: `Bearer ${finalToken}` }),
      },
      body: passJsonBody ? JSON.stringify(params) : undefined,
      next: { revalidate: cache },
    })

    const resString = await res.text()
    const resJson = resString.length > 0 ? JSON.parse(resString) : {}

    if (!res.ok) {
      switch (res.status) {
        case 401:
          return { data: null, error: 'unauthorized', ok: false } as ApiResult<TResponse>
        case 403:
          return { data: null, error: 'forbidden', ok: false } as ApiResult<TResponse>
        case 404:
          return { data: null, error: 'not_found', ok: false } as ApiResult<TResponse>
        default:
          throw `Unknown response status code: '${res.status}', with message: '${resJson["details"] ?? resJson["title"] ?? resString}`
      }
    }
    return { data: resJson as TResponse, error: null, ok: true } as ApiResult<TResponse>
  } catch (e) {
    if (e instanceof SyntaxError) {
      return { data: null, error: `Syntax error when tried to parse JSON: ${e.message}`, ok: false } as ApiResult<TResponse>
    }
    return { data: null, error: `Unexpected error: ${e}`, ok: false } as ApiResult<TResponse>
  }
}

export {
  type ApiResult,
  
  api
}
