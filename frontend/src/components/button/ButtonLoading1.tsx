import { Loader2 } from 'lucide-react'
import { Button } from '@/components/ui/button'
import 'tailwindcss/tailwind.css'

interface LoadingButtonProps {
  onClick: () => void;
  children?: React.ReactNode;
  nameButton: string;
  classCSS?: string;
  isLoading: boolean;
}

export default function ButtonLoading1({ onClick, children, nameButton, classCSS, isLoading }: LoadingButtonProps) {
  return (
    <Button
      className={classCSS}
      onClick={onClick}
      disabled={isLoading}
    >
      {children}
      {isLoading ? (
        <Loader2 className="mr-2 h-4 w-4 animate-spin" />
      ) : (
        nameButton
      )}
    </Button>
  )
}