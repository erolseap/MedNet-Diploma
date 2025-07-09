import { AlertCircleIcon } from "lucide-react";
import { Alert, AlertDescription, AlertTitle } from "./ui/alert";

export default function FailAlert({
  title,
  children = null,
  ...props
}: React.ComponentProps<"div"> & {
  title: string;
}) {

  return <Alert variant="destructive" {...props}>
    <AlertCircleIcon />
    <AlertTitle>{title}</AlertTitle>
    <AlertDescription>
      {
        children ?? <p>Unknown error happened. Please try again later.</p>
      }
    </AlertDescription>
  </Alert>
}
