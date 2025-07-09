"use client";

import { fetchGeneratableUserTests, generateUserTest } from "@/lib/api/pub/user-tests";
import QuestionsSetWithoutQuestionsDto from "@/lib/dtos/questions-set-without-questions.dto";
import * as DialogPrimitive from "@radix-ui/react-dialog";
import { FormEvent, useEffect, useState } from "react";
import FailAlert from "./fail-alert";
import { Button } from "./ui/button";
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle } from "./ui/dialog";
import { Input } from "./ui/input";
import { Label } from "./ui/label";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "./ui/select";

export default function UserTestGenerationDialog({
  onSubmit,
  onCompleted,
  ...props
}: React.ComponentProps<typeof DialogPrimitive.Root> & {
  onSubmit?: (questionsSetId: number, amountOfQuestions: number) => void;
  onCompleted?: (createdId: number) => void;
}) {
  if ((onSubmit && onCompleted) || (!onSubmit && !onCompleted)) {
    throw new Error(
      "Exactly one of `onSubmit` or `onCompleted` must be provided, but not both."
    );
  }

  const [requestFailed, setRequestFailed] = useState(false);
  const [generatableItems, setGeneratableItems] = useState<QuestionsSetWithoutQuestionsDto[] | undefined>(undefined);

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetchGeneratableUserTests();
      if (response.ok) {
        setGeneratableItems(response.data);
      }
    }
    fetchData()
  }, []);

  async function handleForm(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setRequestFailed(false);

    const formData = new FormData(event.currentTarget);
    const questionsSetId = Number(formData.get('questionsSetId'));
    const amountOfQuestions = Number(formData.get('amountOfQuestions'));

    if (onSubmit != null) {
      onSubmit(questionsSetId, amountOfQuestions);
    } else if (onCompleted != null) {
      const response = await generateUserTest({
        setId: questionsSetId,
        numOfQuestions: amountOfQuestions
      });
      if (response.ok) {
        onCompleted(response.data.id);
      } else {
        setRequestFailed(true);
      }
    }
  }

  return (
    <Dialog {...props}>
      <DialogContent className="sm:max-w-sm">
        <DialogHeader>
          <DialogTitle>Generate a new test</DialogTitle>
          <DialogDescription>
            Select a questions set you would like to generate questions based on, and specify an amount of questions you would like to generate in.
          </DialogDescription>
        </DialogHeader>
        <form onSubmit={handleForm}>
          <div className="flex flex-col gap-6">
            <div className="grid gap-3">
              <Label htmlFor="questionsSetId">Questions set</Label>
              <Select
                name="questionsSetId"
                required>
                <SelectTrigger
                  className="w-full"
                  name="questionsSetId"
                  id="questionsSetId"
                  disabled={generatableItems === undefined}
                >
                  <SelectValue placeholder="Select a preferred questions set" />
                </SelectTrigger>
                <SelectContent>
                  {
                    generatableItems && generatableItems.filter((item) => item.numOfQuestions > 0).map((item) =>
                      <SelectItem key={item.id} value={`${item.id}`}>{item.name} ({item.numOfQuestions})</SelectItem>
                    )
                  }
                </SelectContent>
              </Select>
            </div>
            <div className="grid gap-3">
              <Label htmlFor="amountOfQuestions">Amount of questions</Label>
              <Input
                className="w-full"
                id="amountOfQuestions"
                name="amountOfQuestions"
                type="number"
                min={5}
                max={300}
                required />
            </div>
            {
              requestFailed &&
              <FailAlert title="Unable to generate a new test." />
            }
            <Button type="submit" className="w-full" disabled={generatableItems === undefined}>
              Generate
            </Button>
          </div>
        </form>
      </DialogContent>
    </Dialog>
  )
}

