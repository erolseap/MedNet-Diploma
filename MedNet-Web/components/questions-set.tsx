import React from 'react'
import { Button } from './ui/button';
import Link from 'next/link';

interface QuestionsSetProps {
  id: number;
  name: string;
  numOfQuestions: number;
}

export default function QuestionsSet({ id, name, numOfQuestions }: QuestionsSetProps) {
  return (
    <>
      <Button variant="outline-primary" className="py-6 w-full overflow-hidden flex items-center justify-between gap-3" asChild>
        <Link href={`/admin/sets/${id}`}>
          <p className="truncate flex-1">{name}</p>
          <p className="text-muted-foreground flex-shrink-0">
            {numOfQuestions}
          </p>
        </Link>
      </Button>
    </>
  )
}

