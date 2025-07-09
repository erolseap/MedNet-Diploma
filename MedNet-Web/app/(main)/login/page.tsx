import LoginForm from "@/components/login-form";

export default function Login() {
  return (
    <div className="flex flex-col gap-16">
      <div className="flex items-center justify-center min-h-screen p-8 pb-20 gap-16 sm:p-20 font-[family-name:var(--font-geist-sans)]">
        <div className="w-full max-w-sm">
          <LoginForm />
        </div>
      </div>
    </div>
  );
}
