'use client'
import * as React from 'react';



export const UseSnakeBarModal = (isLoadingApiFetchLoginAndRegister: boolean, MessageApiAuth: string | null) =>
{
    const [open, setOpen] = React.useState(false);
    React.useEffect(() => {
    if (!isLoadingApiFetchLoginAndRegister && MessageApiAuth) 
        {
            setOpen(true);
            const timer = setTimeout(() => {
                setOpen(false);
            }, 5000);
        
            return () => clearTimeout(timer);
        }
    }, [isLoadingApiFetchLoginAndRegister, MessageApiAuth])
    
    return {open};
};