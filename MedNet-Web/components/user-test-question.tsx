'use client'

import { answerUserTestQuestion } from '@/lib/api/pub/user-tests'
import UserTestSessionQuestionDto from '@/lib/dtos/user-test-session-question.dto'
import { useState } from 'react'
import Markdown from 'react-markdown'
import { Button } from './ui/button'
import { Card, CardContent } from './ui/card'

export default function UserTestQuestion({
  className,
  value,
  testId,
  onAnswered
}: {
  className?: string;
  value: UserTestSessionQuestionDto;
  testId: number;
  onAnswered?: (correct: boolean) => void
}) {
  const [props, setProps] = useState(value);

  async function handleAnswerClick(id: number) {
    const request = await answerUserTestQuestion({
      answerId: id,
      questionId: props.id,
      id: testId
    })
    if (request.ok) {
      setProps({...props, correctAnswerId: request.data.correctAnswerId, selectedAnswerId: request.data.selectedAnswerId});
      if (onAnswered != null) {
        onAnswered(request.data.correctAnswerId === id);
      }
    }
  }

  return (
    <Card className={className}>
      <CardContent className='flex flex-col gap-2'>
        <div className="flex flex-row items-start gap-1">
          <div className="text-muted-foreground">{props.blankQuestionNumber}.</div>
          <Markdown
            components={{
              pre: (props) => (
                <pre
                  {...props}
                  className="whitespace-pre-wrap break-words max-w-full"
                />
              ),
              code: (props) => (
                <code
                  {...props}
                  className="break-words whitespace-normal"
                />
              ),
            }}>{props.body}</Markdown>
        </div>
        <div className='flex flex-col gap-2'>
          {
            props.answers.map((item, index) => {
              let variant: "positive" | "destructive" | "outline" = "outline";
              if (props.correctAnswerId === item.id) {
                variant = "positive";
              } else if (props.selectedAnswerId === item.id) {
                variant = "destructive";
              }
              return <Button
                variant={variant}
                key={item.id} className="w-auto min-h-12 text-start justify-start max-h-none whitespace-normal h-auto flex flex-row gap-1"
                disabled={props.selectedAnswerId != null}
                onClick={() => handleAnswerClick(item.id)}
              >
                <div className="text-muted-foreground">{`${String.fromCharCode(65 + index)})`}</div>
                <Markdown
                  components={{
                    pre: (props) => (
                      <pre
                        {...props}
                        className="whitespace-pre-wrap break-words max-w-full"
                      />
                    ),
                    code: (props) => (
                      <code
                        {...props}
                        className="break-words whitespace-normal"
                      />
                    ),
                  }}>{item.body}</Markdown>
              </Button>
            })
          }
        </div>
      </CardContent>
    </Card>
  )
}
