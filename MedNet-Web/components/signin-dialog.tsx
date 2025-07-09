"use client";

import { Label } from "./ui/label";
import Link from "next/link";
import { Button } from "./ui/button";
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle } from "./ui/dialog";
import { Input } from "./ui/input";
import * as DialogPrimitive from "@radix-ui/react-dialog"
import { FormEvent } from "react";
import { loginWithCredentials } from "@/lib/api/pub/auth";
import { saveAccessToken } from "@/lib/api/auth";

export default function SigninDialog({
  onSubmit,
  onCompleted,
  onSignupDialogRequested,
  ...props
}: React.ComponentProps<typeof DialogPrimitive.Root> & {
  onSubmit?: (email: string, password: string) => void;
  onCompleted?: () => void;
  onSignupDialogRequested?: () => void;
}) {
  if ((onSubmit && onCompleted) || (!onSubmit && !onCompleted)) {
    throw new Error(
      "Exactly one of `onSubmit` or `onCompleted` must be provided, but not both."
    );
  }

  async function handleForm(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    const formData = new FormData(event.currentTarget);
    const email = formData.get('email') as string;
    const password = formData.get('password') as string;

    if (onSubmit != null) {
      onSubmit(email, password);
    } else if (onCompleted != null) {
      var authorizationResult = await loginWithCredentials({
        email: email,
        password: password
      });
      if (authorizationResult.ok) {
        saveAccessToken(authorizationResult.data.accessToken);
        onCompleted();
      }
    }
  }

  return (
    <Dialog {...props}>
      <DialogContent className="sm:max-w-sm">
        <DialogHeader>
          <DialogTitle>Login to your account</DialogTitle>
          <DialogDescription>
            Enter your email and password below to login to your account.
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
              Sign in
            </Button>
          </div>
          {
            onSignupDialogRequested != null
            && <div className="mt-4 text-center text-sm content-center">
              Don&apos;t have an account?{" "}
              <Link href="#" className="underline underline-offset-4" onClick={(e) => { e.preventDefault(); onSignupDialogRequested(); }}>
                Sign up
              </Link>
            </div>
          }
        </form>
      </DialogContent>
    </Dialog>
  )
}
