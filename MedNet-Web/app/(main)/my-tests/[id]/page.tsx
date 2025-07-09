"use client"

import { Button } from "@/components/ui/button";
import { Skeleton } from "@/components/ui/skeleton";
import { ToggleGroup, ToggleGroupItem } from "@/components/ui/toggle-group";
import UserTestQuestion from "@/components/user-test-question";
import { fetchUserTest, fetchUserTestQuestions, generateMoreUserTestQuestions } from "@/lib/api/pub/user-tests";
import UserTestSessionQuestionDto from "@/lib/dtos/user-test-session-question.dto";
import UserTestSessionDto from "@/lib/dtos/user-test-session.dto";
import { shuffleArray } from "@/lib/utils";
import { CheckIcon, CircleQuestionMarkIcon, ListIcon, PickaxeIcon, XIcon } from "lucide-react";
import { notFound, useParams } from "next/navigation";
import { useEffect, useRef, useState } from "react";

export default function Page() {
  const params = useParams();
  const id = Number(params.id);

  const [testData, setTestData] = useState<UserTestSessionDto | undefined | null>(undefined);
  const [testQuestions, setTestQuestions] = useState<UserTestSessionQuestionDto[] | undefined>(undefined);
  const [displayMode, setDisplayMode] = useState<"all" | "unanswered-only" | "wrong-only" | "correct-only">("all");

  const fetchByAnsweredTimeoutRef = useRef<NodeJS.Timeout | undefined>(undefined);

  function handleAnswered() {
    if (fetchByAnsweredTimeoutRef.current) {
      clearTimeout(fetchByAnsweredTimeoutRef.current);
    }

    fetchByAnsweredTimeoutRef.current = setTimeout(() => {
      fetchTestQuestions();
      fetchByAnsweredTimeoutRef.current = undefined;
    }, 2000);
  };

  async function fetchTestQuestions() {
    const questionsData = await fetchUserTestQuestions({ id: id });
    if (questionsData.ok) {
      setTestQuestions(questionsData.data);
    }
  }

  useEffect(() => {
    const fetchTestData = async () => {
      const testData = await fetchUserTest({ id: id });
      if (testData.ok) {
        setTestData(testData.data);
      } else if (testData.error == 'not_found') {
        setTestData(null);
      }
    };

    fetchTestData();
    fetchTestQuestions();
  }, []);

  async function generateMoreQuestions() {
    const result = await generateMoreUserTestQuestions({ id: id });
    if (result.ok) {
      fetchTestQuestions();
    }
  }

  if (!Number.isFinite(id) || id <= 0) {
    return notFound();
  }

  if (testData === null) {
    return notFound();
  }

  return (
    <div className="flex flex-col items-center gap-3">
      <div className="flex flex-row justify-between w-full max-w-3xl items-center gap-5">
        {
          testData == null ?
            <Skeleton className="w-60 h-[3rem]" />
            : <p className="text-2xl">{testData.parentQuestionsSet.name}</p>
        }
        {
          testData == null ?
            <Skeleton className="w-60 h-[3rem]" />
            : <ToggleGroup
              type="single"
              variant="outline"
              onValueChange={(v: "all" | "unanswered-only" | "wrong-only" | "correct-only") => setDisplayMode(v)} value={displayMode}
            >
              <ToggleGroupItem value="all" className={displayMode == "all" ? undefined : "hover:cursor-pointer"}><ListIcon /></ToggleGroupItem>
              <ToggleGroupItem value="unanswered-only" className={displayMode == "unanswered-only" ? undefined : "hover:cursor-pointer"}><CircleQuestionMarkIcon /></ToggleGroupItem>
              <ToggleGroupItem value="wrong-only" className={displayMode == "wrong-only" ? undefined : "hover:cursor-pointer"}><XIcon /></ToggleGroupItem>
              <ToggleGroupItem value="correct-only" className={displayMode == "correct-only" ? undefined : "hover:cursor-pointer"}><CheckIcon /></ToggleGroupItem>
            </ToggleGroup>
        }
      </div>
      <div className="flex flex-col gap-3 max-w-2xl w-full mb-3 items-center">
        {
          testQuestions == undefined ?
            Array.from({ length: 20 }).map((_, i) => (
              <Skeleton key={i} className="w-full h-25 rounded-xl" />
            ))
            :
            testQuestions.filter((question) =>
              displayMode == "correct-only" ? (question.selectedAnswerId !== null && question.correctAnswerId === question.selectedAnswerId)
                : displayMode == "wrong-only" ? (question.selectedAnswerId !== null && question.correctAnswerId !== question.selectedAnswerId)
                  : displayMode == "unanswered-only" ? (question.selectedAnswerId === null) : true
            ).map((question) => (
              <UserTestQuestion
                key={question.id} value={{
                  ...question,
                  answers: shuffleArray([...question.answers]),
                }}
                testId={id}
                onAnswered={() => handleAnswered()}
                className="w-full" />
            ))
        }
        {
          (displayMode == "all" || displayMode == "unanswered-only")
          && <Button
            className="max-w-xl w-full flex flex-row gap-1"
            variant="ghost"
            onClick={() => generateMoreQuestions()}
          >
            <PickaxeIcon />
            <div>Generate more...</div>
          </Button>
        }
      </div>
    </div>
  )
}
