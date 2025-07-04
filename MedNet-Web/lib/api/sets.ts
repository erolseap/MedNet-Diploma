import QuestionDto from "../dtos/question.dto";
import QuestionsSetWithoutQuestionsDto from "../dtos/questions-set-without-questions.dto";
import { api, ApiResult } from "./api"

interface PaginationParams {
    offset?: number;
    limit?: number;
}

/// QuestionsSetsController

async function fetchQuestionsSets(cache: number = 5): Promise<ApiResult<QuestionsSetWithoutQuestionsDto[]>> {
    return await api<QuestionsSetWithoutQuestionsDto[]>('/sets', undefined, undefined, cache)
}

async function createQuestionsSet(name: string): Promise<ApiResult<void>> {
    return await api<void>('/sets', {
        name: name
    }, 'POST')
}

/// QuestionsSetController

async function fetchQuestionsSet(id: number, cache: number = 5): Promise<ApiResult<QuestionsSetWithoutQuestionsDto>> {
    return await api<QuestionsSetWithoutQuestionsDto>(`/sets/${id}`, undefined, undefined, cache)
}

async function deleteQuestionsSet(id: number): Promise<ApiResult> {
    return await api(`/sets/${id}`, undefined, 'DELETE')
}

async function fetchQuestionsSetQuestions(id: number, pagination?: PaginationParams, cache: number = 5): Promise<ApiResult<QuestionDto[]>> {
    return await api<QuestionDto[]>(`/sets/${id}/questions`, {
        offset: pagination?.offset,
        limit: pagination?.limit
    }, undefined, cache)
}

export {
    fetchQuestionsSets,
    createQuestionsSet,

    fetchQuestionsSet,
    deleteQuestionsSet,
    fetchQuestionsSetQuestions
}

