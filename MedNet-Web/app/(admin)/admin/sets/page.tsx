"use client"

import QuestionsSet from "@/components/questions-set"
import { Button } from "@/components/ui/button"
import { Card, CardHeader, CardTitle, CardContent, CardAction } from "@/components/ui/card"
import { DialogHeader, DialogFooter } from "@/components/ui/dialog"
import { Input } from "@/components/ui/input"
import { Dialog, DialogTrigger, DialogContent, DialogTitle, DialogDescription, DialogClose } from "@/components/ui/dialog"
import { Label } from "@radix-ui/react-label"
import { FormEvent, useEffect, useState } from "react"
import { createQuestionsSet, fetchQuestionsSets, QuestionsSetListEntry } from "@/lib/api/sets"
import { toast } from "sonner"
import { Skeleton } from "@/components/ui/skeleton"

export const dynamicParams = true;

export default function Page() {
  const [questionsSets, setQuestionsSets] = useState<QuestionsSetListEntry[]|undefined>(undefined)

  async function handleCreateForm(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()

    const formData = new FormData(event.currentTarget)
    const name = formData.get('name') as string

    const response = await createQuestionsSet(name)
    console.log(response)
    if (response.ok) {
      toast(`Successfully created a questions set named as '${name}'.`)
      setTimeout(function () {
        window.location.reload();
      }, 2000);
    }
  }

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetchQuestionsSets()
      if (response.ok) {
        setQuestionsSets(response.data)
      }
    }
    fetchData()
  }, [])

  return (
    <>
      <Card className="@container w-full">
        <CardHeader className="flex justify-between items-center">
          <CardTitle className="text-xl">Questions sets list</CardTitle>
          <CardAction>
            <Dialog>
              <DialogTrigger asChild>
                <Button variant="outline" withHoverEffect>
                  Create
                </Button>
              </DialogTrigger>
              <DialogContent className="sm:max-w-sm">
                <DialogHeader>
                  <DialogTitle>Create a new questions set</DialogTitle>
                  <DialogDescription>
                    Click save when you&apos;re done.
                  </DialogDescription>
                </DialogHeader>
                <form onSubmit={handleCreateForm} className="grid gap-4">
                  <div className="grid gap-3">
                    <Label htmlFor="name">Name</Label>
                    <Input id="name" name="name" maxLength={48} />
                  </div>
                  <DialogFooter>
                    <DialogClose asChild>
                      <Button variant="outline">Cancel</Button>
                    </DialogClose>
                    <DialogClose asChild>
                      <Button type="submit">Save changes</Button>
                    </DialogClose>
                  </DialogFooter>
                </form>
              </DialogContent>
            </Dialog>
          </CardAction>
        </CardHeader>
        <CardContent className="gap-3 grid @md:grid-cols-[repeat(auto-fit,minmax(23.2rem,1fr))]">
          {
            questionsSets == null ?
              Array.from({ length: 20 }).map((_, i) => (
                  <Skeleton key={i} className="w-full h-13 rounded-xl" />
              ))
            :
              questionsSets.map((set) => (
                <QuestionsSet
                  key={set.id}
                  id={set.id}
                  name={set.name}
                  numOfQuestions={set.numOfQuestions} />
              ))
          }
        </CardContent>
      </Card>
    </>
  )
}
