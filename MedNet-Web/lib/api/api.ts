export type ApiResult<T = void> =
  T extends void
  ? { error: null; ok: true } | { error: string; ok: false }
  : { data: T; error: null; ok: true } | { data: null; error: string; ok: false };

export async function api<TResponse = void>(
  path: string,
  params?: Record<string, any>,
  type: string = "GET",
  cache: number = 0
): Promise<ApiResult<TResponse>> {
  if (!path.startsWith('/')) path = `/${path}`;

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
      },
      body: passJsonBody ? JSON.stringify(params) : undefined,
      next: { revalidate: cache },
    });

    const resString = await res.text()
    const resJson = resString.length > 0 ? JSON.parse(resString) : {}

    if (!res.ok) {
      throw `Unknown response status code: '${res.status}', with message: '${resJson["details"] ?? resJson["title"] ?? resString}`
    }
    return { data: resJson as TResponse, error: null, ok: true } as ApiResult<TResponse>
  } catch (e) {
    if (e instanceof SyntaxError) {
      return { data: null, error: `Syntax error when tried to parse JSON: ${e.message}`, ok: false } as ApiResult<TResponse>
    }
    return { data: null, error: `Unexpected error: ${e}`, ok: false } as ApiResult<TResponse>
  }
}

