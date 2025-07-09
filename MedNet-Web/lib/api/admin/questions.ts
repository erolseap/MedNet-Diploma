import QuestionDto from "../../dtos/question.dto"
import { api, ApiResult } from "../api"

/// QuestionController

async function fetchQuestion({setId, id, cache, token} : { setId: number; id: number; cache: number; token?: string; }): Promise<ApiResult<QuestionDto>> {
    return await api<QuestionDto>(`/sets/${setId}/questions/${id}`, {
        cache: cache,
        token: token
    })
}

async function deleteQuestion({ setId, id, token }: { setId: number; id: number, token?: string; }): Promise<ApiResult> {
    return await api(`/sets/${setId}/questions/${id}`, {
        type: 'DELETE',
        token: token
    })
}

export {
    fetchQuestion,
    deleteQuestion
}
