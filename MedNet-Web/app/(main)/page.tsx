import { Alert, AlertTitle, AlertDescription } from "@/components/ui/alert";
import { HelpCircleIcon } from "lucide-react";
import Link from "next/link";

export default function Home() {
  return (
    <div className="flex flex-col items-center justify-center min-h-screen">
      <Alert variant="default" className="text-center max-w-sm">
        <HelpCircleIcon />
        <AlertTitle>Hello!</AlertTitle>
        <AlertDescription className="justify-items-center">
          <p>Welcome to <Link href="/" className="underline underline-offset-4">MedNet.</Link></p>
          <p>Please sign up in our platform, or sign in to your account if you have any, to access all website features.</p>
        </AlertDescription>
      </Alert>
    </div>
  );
}
