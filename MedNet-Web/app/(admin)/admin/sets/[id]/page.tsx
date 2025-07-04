"use client"

import Question from "@/components/question"
import { Button } from "@/components/ui/button"
import { Edit2, Trash2 } from "lucide-react"

export default function Page() {
  return (
    <div className="flex flex-col items-center gap-3">
      <div className="flex flex-row justify-between w-full max-w-3xl items-center">
        <p className="text-2xl">fasf</p>
        <div className="flex flex-row gap-3 items-center">
          <Button
            className="h-9 group-data-[collapsible=icon]:opacity-0"
            variant="outline"
          >
            <Edit2 /> Edit
          </Button>
          <Button
            size="icon"
            className="h-9 group-data-[collapsible=icon]:opacity-0"
            variant="destructive"
          >
            <Trash2 />
          </Button>
        </div>
      </div>
      <div className="flex flex-col gap-3 max-w-2xl w-full">
        <Question id={1} body="Lorem ipsum" type='single-choice' answers={[]} />
        <Question id={1} body="Lorem ipsum" type='single-choice' answers={[]} />
        <Question id={1} body="Lorem ipsum" type='single-choice' answers={[]} />
        <Question id={1} body="Lorem ipsum" type='single-choice' answers={[]} />
        <Question id={1} body="Lorem ipsum" type='single-choice' answers={[]} />
        <Question id={1} body="Lorem ipsum" type='single-choice' answers={[]} />
        <Question id={1} body="Lorem ipsum" type='single-choice' answers={[]} />
        <Question id={1} body="Lorem ipsum" type='single-choice' answers={[]} />
        <Question id={1} body="Lorem ipsum" type='single-choice' answers={[]} />
        <Question id={1} body="Lorem ipsum" type='single-choice' answers={[]} />

      </div>
    </div>
  )
}
