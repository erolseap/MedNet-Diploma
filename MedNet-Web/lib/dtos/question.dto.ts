import AnswerDto from "./answer.dto";

export default interface QuestionDto {
    id: number;
    body: string;
    type: 'single-choice' | 'multiple-choice' | 'free-text';
    answers: AnswerDto[];
}
