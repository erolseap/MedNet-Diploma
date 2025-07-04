import QuestionDto from "../dtos/question.dto"
import { api, ApiResult } from "./api"

/// QuestionController

async function fetchQuestion(setId: number, id: number, cache: number = 5): Promise<ApiResult<QuestionDto>> {
    return await api<QuestionDto>(`/sets/${setId}/questions/${id}`, undefined, undefined, cache)
}

async function deleteQuestion(setId: number, id: number): Promise<ApiResult> {
    return await api(`/sets/${setId}/questions/${id}`, undefined, 'DELETE')
}

export {
    fetchQuestion,
    deleteQuestion
}
