'use client'

import { Geist, Geist_Mono } from "next/font/google";
import "../../globals.css";
import { Toaster } from "@/components/ui/sonner";
import Header from "@/components/header";
import { Breadcrumb, BreadcrumbItem, BreadcrumbLink, BreadcrumbList, BreadcrumbPage, BreadcrumbSeparator } from "@/components/ui/breadcrumb";
import { usePathname } from "next/navigation";
import { Fragment } from "react";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export default function RootLayout({
  children,
  params
}: Readonly<{
  children: React.ReactNode
  params: { slug?: string[] }
}>) {
  const paths = usePathname()
  const pathNames = paths.split('/').filter( path => path )
  
  return (
    <html lang="en" className="dark">
      <body
        className={`${geistSans.variable} ${geistMono.variable} antialiased`} style={{ backgroundImage: "radial-gradient(circle, var(--secondary) 1px, transparent 1px)", backgroundSize: "15px 15px" }}
      >
        <div className="flex flex-col justify-center items-center px-2">
          <Header className="max-w-6xl mt-3 w-full pb-3 px-2" />
          <main className="block max-w-5xl items-center w-full">
            <Breadcrumb className="block w-full p-3">
              <BreadcrumbList>
                {
                  pathNames.map((link, index) => {
                    let href = `/${pathNames.slice(0, index + 1).join('/')}`
                    let itemLink = link[0].toUpperCase() + link.slice(1, link.length)

                    return <Fragment key={index}>
                      <BreadcrumbItem>
                        <BreadcrumbLink href={href}>{itemLink}</BreadcrumbLink>
                      </BreadcrumbItem>
                      {
                        index < pathNames.length - 1 && <BreadcrumbSeparator />
                      }
                    </Fragment>
                  })
                }
              </BreadcrumbList>
            </Breadcrumb>
            {children}
          </main>
        </div>
        <Toaster />
      </body>
    </html>
  );
}
