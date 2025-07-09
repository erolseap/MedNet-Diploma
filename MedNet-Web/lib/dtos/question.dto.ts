import AnswerDto from "./answer.dto";

export default interface QuestionDto {
    id: number;
    body: string;
    answers: AnswerDto[];
}
