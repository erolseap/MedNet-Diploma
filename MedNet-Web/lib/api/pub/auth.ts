import { api, ApiResult } from "../api"

interface AuthorizationCredentials {
    tokenType: string;
    accessToken: string;
    expiresIn: number;
    refreshToken: string;
}

async function loginWithCredentials({email, password} : { email: string; password: string; }): Promise<ApiResult<AuthorizationCredentials>> {
    return await api<AuthorizationCredentials>(`/account/login`, {
        params: {
            email: email,
            password: password
        },
        type: 'POST',
        token: null
    })
}

async function registerWithCredentials({email, password} : { email: string; password: string; }): Promise<ApiResult> {
    return await api(`/account/register`, {
        params: {
            email: email,
            password: password
        },
        type: 'POST',
        token: null
    })
}

async function checkAuthorization({token} : { token?: string; } = {}): Promise<ApiResult> {
    return await api(`/account/manage/info`, {
        token: token
    })
}

export {
    loginWithCredentials,
    registerWithCredentials,
    checkAuthorization
}
