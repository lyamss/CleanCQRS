import { LoaderCustombg } from "@/components/ui/LoaderCustombg";
import { Skeleton } from "@mui/material";

export const LoadingPagePrincipale = () =>
    {
      return (
        <div className="min-h-screen bg-gray-100 py-12 px-4 sm:px-6 lg:px-8">
          <div className="max-w-7xl mx-auto">
        <div className='items-center'>
         <LoaderCustombg/>
        </div>
      <div className="flex items-stretch space-x-4">
        <Skeleton variant="rectangular" className='py-4' width={410} height={218} />
        <Skeleton variant="rectangular" className='py-12' width={410} height={218} />
        <Skeleton variant="rectangular" className='py-8' width={410} height={218} />
      </div>
      </div>
      </div>
      );
}