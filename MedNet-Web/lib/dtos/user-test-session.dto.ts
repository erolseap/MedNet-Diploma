import QuestionsSetWithoutQuestionsDto from "./questions-set-without-questions.dto";

export default interface UserTestSessionDto {
    id: number;
    parentQuestionsSet: QuestionsSetWithoutQuestionsDto;
    creationDate: string;
    numOfQuestions: number;
    correctAnswersCount: number;
}
