'use client'

import React from 'react'
import { Card, CardContent } from './ui/card'
import { Button } from './ui/button'
import Markdown from 'react-markdown'
import UserTestSessionQuestionDto from '@/lib/dtos/user-test-session-question.dto'

export default function TestQuestion(props: UserTestSessionQuestionDto) {
  return (
    <Card>
      <CardContent className='flex flex-col gap-2'>
        <Markdown>{`${item.blankQuestionNumber}. ${item.body}`}</Markdown>
        <div className='flex flex-col gap-2'>
          <Button variant="outline">fasf</Button>
          <Button variant="outline">fasf</Button>
        </div>
      </CardContent>
    </Card>
  )
}
