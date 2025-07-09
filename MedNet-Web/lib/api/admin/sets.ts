import QuestionDto from "../../dtos/question.dto";
import QuestionsSetWithoutQuestionsDto from "../../dtos/questions-set-without-questions.dto";
import { api, ApiResult } from "../api"

interface PaginationParams {
    offset?: number;
    limit?: number;
}

/// QuestionsSetsController

async function fetchQuestionsSets({ cache = 5, token }: { cache?: number; token?: string; } = {}): Promise<ApiResult<QuestionsSetWithoutQuestionsDto[]>> {
    return await api<QuestionsSetWithoutQuestionsDto[]>('/sets', {
        cache: cache,
        token: token
    })
}

async function createQuestionsSet({ name, token }: { name: string; token?: string; }): Promise<ApiResult<void>> {
    return await api<void>('/sets', {
        params: {
            name: name
        },
        type: 'POST',
        token: token
    })
}

/// QuestionsSetController

async function fetchQuestionsSet({ id, cache = 5, token }: { id: number; cache?: number; token?: string; }): Promise<ApiResult<QuestionsSetWithoutQuestionsDto>> {
    return await api<QuestionsSetWithoutQuestionsDto>(`/sets/${id}`, {
        cache: cache,
        token: token
    })
}

async function deleteQuestionsSet(id: number): Promise<ApiResult> {
    return await api(`/sets/${id}`, {
        type: 'DELETE'
    })
}

async function fetchQuestionsSetQuestions({ id, pagination, cache = 5, token }: { id: number; pagination?: PaginationParams; cache?: number; token?: string; }): Promise<ApiResult<QuestionDto[]>> {
    return await api<QuestionDto[]>(`/sets/${id}/questions`, {
        params: {
            offset: pagination?.offset,
            limit: pagination?.limit
        },
        cache: cache,
        token: token
    })
}

export {
    fetchQuestionsSets,
    createQuestionsSet,

    fetchQuestionsSet,
    deleteQuestionsSet,
    fetchQuestionsSetQuestions
}

