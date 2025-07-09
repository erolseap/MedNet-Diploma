"use client";

import { Button } from "@/components/ui/button"
import { Skeleton } from "@/components/ui/skeleton";
import UserTestCard from "@/components/user-test-card"
import UserTestGenerationDialog from "@/components/user-test-generation-dialog";
import { fetchUserTests } from "@/lib/api/pub/user-tests";
import UserTestSessionDto from "@/lib/dtos/user-test-session.dto";
import { useEffect, useState } from "react";
import { toast } from "sonner";

export default function Page() {
  const [isGenerationDialogDisplayed, setGenerationDialogDisplayed] = useState(false);
  const [userTestsList, setUserTestsList] = useState<UserTestSessionDto[] | undefined>(undefined);

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetchUserTests();
      if (response.ok) {
        setUserTestsList(response.data);
      }
    }
    fetchData()
  }, []);

  return (
    <div className="@container w-full">
      <div className="flex justify-between items-center mb-4">
        <p className="text-xl">My Tests</p>
        <Button variant='outline' onClick={() => setGenerationDialogDisplayed(true)}>Generate</Button>
      </div>
      <div className="w-full gap-3 grid @md:grid-cols-[repeat(auto-fit,minmax(20.2rem,1fr))]">
        {
          userTestsList == undefined ?
            Array.from({ length: 20 }).map((_, i) => (
              <Skeleton key={i} className="w-full h-13 rounded-xl" />
            ))
            :
            userTestsList.map((test) => (
              <UserTestCard
                key={test.id}
                id={test.id}
                name={test.parentQuestionsSet.name}
                numOfQuestions={test.numOfQuestions}
                correctAnswersCount={test.correctAnswersCount}
                creationDate={test.creationDate} />
            ))
        }
      </div>
      <UserTestGenerationDialog
        open={isGenerationDialogDisplayed}
        onOpenChange={(display) => setGenerationDialogDisplayed(display)}
        onCompleted={() => {
          setGenerationDialogDisplayed(false);
          toast("Successfully generated!");
          setTimeout(function () {
            window.location.reload();
          }, 2000);
        }} />
    </div>
  )
}
