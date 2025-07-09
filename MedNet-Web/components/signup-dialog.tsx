"use client";

import { Label } from "./ui/label";
import Link from "next/link";
import { Button } from "./ui/button";
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle } from "./ui/dialog";
import { Input } from "./ui/input";
import * as DialogPrimitive from "@radix-ui/react-dialog"
import { FormEvent, useState } from "react";
import { registerWithCredentials } from "@/lib/api/pub/auth";
import { Alert, AlertDescription, AlertTitle } from "./ui/alert";
import { AlertCircleIcon, Terminal } from "lucide-react";

export default function SignupDialog({
  onSubmit,
  onCompleted,
  onSigninDialogRequested,
  ...props
}: React.ComponentProps<typeof DialogPrimitive.Root> & {
  onSubmit?: (email: string, password: string) => void;
  onCompleted?: () => void;
  onSigninDialogRequested?: () => void;
}) {
  if ((onSubmit && onCompleted) || (!onSubmit && !onCompleted)) {
    throw new Error(
      "Exactly one of `onSubmit` or `onCompleted` must be provided, but not both."
    );
  }

  const [signupFailed, setSignupFailed] = useState(false);

  async function handleForm(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setSignupFailed(false);

    const formData = new FormData(event.currentTarget);
    const email = formData.get('email') as string;
    const password = formData.get('password') as string;

    if (onSubmit != null) {
      onSubmit(email, password);
    } else if (onCompleted != null) {
      var authorizationResult = await registerWithCredentials({
        email: email,
        password: password
      });
      if (authorizationResult.ok) {
        onCompleted();
      } else {
        setSignupFailed(true);
      }
    }
  }

  return (
    <Dialog {...props}>
      <DialogContent className="sm:max-w-sm">
        <DialogHeader>
          <DialogTitle>Create a new account</DialogTitle>
          <DialogDescription>
            Enter your email and password below to register a new account.
          </DialogDescription>
        </DialogHeader>
        <form onSubmit={handleForm}>
          <div className="flex flex-col gap-6">
            <div className="grid gap-3">
              <Label htmlFor="email">Email</Label>
              <Input
                id="email"
                name="email"
                type="email"
                placeholder="me@example.com"
                required />
            </div>
            <div className="grid gap-3">
              <Label htmlFor="password">Password</Label>
              <Input
                id="password"
                name="password"
                type="password"
                required />
            </div>
            <Button type="submit" className="w-full">
              Sign up
            </Button>
            {
              signupFailed &&
              <Alert variant="destructive">
                <AlertCircleIcon />
                <AlertTitle>Unable to register a new account.</AlertTitle>
                <AlertDescription>
                  <p>Please verify your email and password and try again.</p>
                  <ul className="list-inside list-disc text-sm">
                    <li>Passwords must have at least one non alphanumeric character.</li>
                    <li>Passwords must have at least one uppercase ('A'-'Z').</li>
                    <li>Passwords must be at least 6 characters long.</li>
                    <li>E-mail address must be unique.</li>
                  </ul>
                </AlertDescription>
              </Alert>
            }
          </div>
          {
            onSigninDialogRequested &&
            <div className="mt-4 text-center text-sm content-center">
              Already have an account?{" "}
              <Link href="#" className="underline underline-offset-4" onClick={(e) => { e.preventDefault(); onSigninDialogRequested(); }}>
                Sign in
              </Link>
            </div>
          }
        </form>
      </DialogContent>
    </Dialog>
  )
}
