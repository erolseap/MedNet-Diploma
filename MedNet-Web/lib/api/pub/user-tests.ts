import UserTestSessionDto from "@/lib/dtos/user-test-session.dto";
import { api, ApiResult } from "../api"
import QuestionsSetWithoutQuestionsDto from "@/lib/dtos/questions-set-without-questions.dto";
import UserTestSessionQuestionDto from "@/lib/dtos/user-test-session-question.dto";

/// UserTestsController

async function fetchUserTests({cache = 5, token}: { cache?: number; token?: string; } = {}): Promise<ApiResult<UserTestSessionDto[]>> {
    return await api<UserTestSessionDto[]>(`/my-tests`, {
        cache: cache,
        token: token
    })
}

async function fetchGeneratableUserTests({cache = 5, token}: { cache?: number; token?: string; } = {}): Promise<ApiResult<QuestionsSetWithoutQuestionsDto[]>> {
    return await api<QuestionsSetWithoutQuestionsDto[]>(`/my-tests/generatable`, {
        cache: cache,
        token: token
    })
}

async function generateUserTest({setId, numOfQuestions = 50, token}: { setId: number; numOfQuestions: number; token?: string; }): Promise<ApiResult<UserTestSessionDto>> {
    return await api<UserTestSessionDto>(`/my-tests/generate`, {
        params: {
            setId: setId,
            numOfQuestions: numOfQuestions
        },
        type: 'POST',
        token: token
    })
}

/// UserTestController

async function fetchUserTest({id, cache = 5, token}: { id: number; cache?: number; token?: string; }): Promise<ApiResult<UserTestSessionDto>> {
    return await api<UserTestSessionDto>(`/my-tests/${id}`, {
        cache: cache,
        token: token
    })
}

async function fetchUserTestQuestions({id, cache = 5, token} : { id: number; cache?: number; token?: string; }): Promise<ApiResult<UserTestSessionQuestionDto[]>> {
    return await api<UserTestSessionQuestionDto[]>(`/my-tests/${id}/questions`, {
        cache: cache,
        token: token
    })
}

async function generateMoreUserTestQuestions({id, cache = 5, token} : { id: number; cache?: number; token?: string; }): Promise<ApiResult> {
    return await api(`/my-tests/${id}/questions/generate-more`, {
        cache: cache,
        token: token,
        type: "POST"
    })
}

/// UserTestQuestionController

async function fetchUserTestQuestion({id, questionId, cache = 5, token} : { id: number; questionId: number; cache?: number; token?: string; }): Promise<ApiResult<UserTestSessionQuestionDto>> {
    return await api<UserTestSessionQuestionDto>(`/my-tests/${id}/questions/${questionId}`, {
        cache: cache,
        token: token
    })
}

async function answerUserTestQuestion({id, questionId, answerId, cache = 5, token} : { id: number; questionId: number; answerId: number; cache?: number; token?: string; }): Promise<ApiResult<UserTestSessionQuestionDto>> {
    return await api<UserTestSessionQuestionDto>(`/my-tests/${id}/questions/${questionId}/answer`, {
        params: {
            answerId: answerId
        },
        type: "POST",
        cache: cache,
        token: token
    })
}

export {
    fetchUserTests,
    fetchGeneratableUserTests,
    generateUserTest,

    fetchUserTest,
    fetchUserTestQuestions,
    generateMoreUserTestQuestions,

    fetchUserTestQuestion,
    answerUserTestQuestion
}
