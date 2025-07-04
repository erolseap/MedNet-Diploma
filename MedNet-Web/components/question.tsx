'use client'

import React from 'react'
import { Card, CardContent, CardHeader, CardTitle } from './ui/card'
import { Button } from './ui/button'
import Markdown from 'react-markdown'

interface Answer {
  id: number
  body: string
  isCorrect: boolean
}

interface QuestionProps {
  id: number
  body: string
  type: 'single-choice' | 'multiple-choice' | 'free-text'
  answers: Answer[]
}

export default function Question({ id, body, type, answers }: QuestionProps) {
  return (
    <Card>
      <CardContent className='flex flex-col gap-2'>
        <Markdown>{body}</Markdown>
        <div className='flex flex-col gap-2'>
          <Button variant="outline">fasf</Button>
          <Button variant="outline">fasf</Button>
        </div>
        {
          answers.map((item, index) => 
            <Button></Button>
          )
        }
      </CardContent>
    </Card>
  )
}
