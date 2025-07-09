import AnswerWithoutStatusDto from "./answer-without-status.dto";

export default interface UserTestSessionQuestionDto {
    id: number;
    body: string;
    blankQuestionNumber: number;
    answers: AnswerWithoutStatusDto[];
    selectedAnswerId: number | null;
    correctAnswerId: number | null;
}
