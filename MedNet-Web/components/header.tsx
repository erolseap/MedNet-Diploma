import { cn } from '@/lib/utils'
import React from 'react'
import { Button } from './ui/button'
import { Card, CardContent } from './ui/card'
import Image from 'next/image'
import Link from 'next/link'

export default function Header({
  className,
  ...props
}: React.ComponentProps<"div">) {
  return (
    <header className={cn("flex flex-col gap-6 ", className)} {...props}>
      <Card>
        <CardContent>
          <div className="flex flex-row justify-between gap-3 items-center">
            <Link href="/">
              <Image src="/logo.svg" alt="MedNet logo" width={100} height={100} />
            </Link>
            {/* <div className="block">
              <NavigationMenu>
                <NavigationMenuList className="gap-3">
                  <NavigationMenuItem>
                    <NavigationMenuLink asChild>
                      <Link href="/docs">Documentation</Link>
                    </NavigationMenuLink>
                  </NavigationMenuItem>
                  <NavigationMenuItem>
                    <NavigationMenuLink asChild>
                      <Link href="/docs">Documentation</Link>
                    </NavigationMenuLink>
                  </NavigationMenuItem>
                </NavigationMenuList>
              </NavigationMenu>
            </div> */}
            <Button variant={"neutral"}>Log in</Button>
          </div>
        </CardContent>
      </Card>
    </header>
  )
}
