'use client'

import { cn } from '@/lib/utils'
import Image from 'next/image'
import Link from 'next/link'
import { useEffect, useState } from 'react'
import SigninDialog from './signin-dialog'
import SignupDialog from './signup-dialog'
import { Button } from './ui/button'
import { Card, CardContent } from './ui/card'
import useAuthorization from '@/lib/hooks/use-authorization'

export default function Header({
  className,
  ...props
}: React.ComponentProps<"div">) {
  const [authorized] = useAuthorization()
  const [displayingDialog, setDisplayingDialog] = useState<"signin" | "signup" | null>(null)

  return (
    <header className={cn("flex flex-col gap-6", className)} {...props}>
      <Card>
        <CardContent>
          <div className="flex flex-row justify-between gap-3 items-center">
            <Link href="/">
              <Image src="/logo.svg" alt="MedNet logo" width={100} height={100} priority />
            </Link>
            {
              authorized
                ?
                <Button variant="neutral" asChild>
                  <Link href="/my-tests">My tests</Link>
                </Button>
                :
                <Button variant="neutral" onClick={() => setDisplayingDialog("signin")}>Sign in</Button>
            }
          </div>
        </CardContent>
      </Card>
      <SigninDialog
        onCompleted={() => { setDisplayingDialog(null); }}
        open={displayingDialog == "signin"}
        onOpenChange={(state) => setDisplayingDialog(state ? "signin" : null)}
        onSignupDialogRequested={() => setDisplayingDialog("signup")} />
      <SignupDialog
        onCompleted={() => { setDisplayingDialog("signin"); }}
        open={displayingDialog == "signup"}
        onOpenChange={(state) => setDisplayingDialog(state ? "signup" : null)}
        onSigninDialogRequested={() => setDisplayingDialog("signin")} />
    </header>
  )
}
