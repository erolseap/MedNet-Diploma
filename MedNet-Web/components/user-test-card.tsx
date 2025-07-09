import Link from "next/link";
import { Button } from "./ui/button";

interface UserTestCardProps {
  id: number;
  name: string;
  creationDate: string;
  numOfQuestions: number;
  correctAnswersCount: number;
}

export default function UserTestCard({ id, name, creationDate, numOfQuestions, correctAnswersCount }: UserTestCardProps) {
  const date = new Date(creationDate);
  const readableCreationDate = date.toLocaleString();

  return (
    <>
      <Button variant="outline-primary" className="py-2 w-full h-full gap-3 flex justify-between overflow-hidden" asChild>
        <Link href={`/my-tests/${id}`}>
          <div className="flex flex-col overflow-hidden">
            <p className="truncate text-lg">{name}</p>
            <p className="truncate text-muted-foreground text-sm">{readableCreationDate}</p>
          </div>
          <div className="flex flex-row items-baseline">
            <div className="text-lg text-positive">{correctAnswersCount}</div>
            {'/'}
            <div className="text-xs">{numOfQuestions}</div>
          </div>
        </Link>
      </Button>
    </>
  )
}
