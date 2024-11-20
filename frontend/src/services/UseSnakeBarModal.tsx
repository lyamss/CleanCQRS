'use client'

import { useGenericEffect } from '@/services/OtherTool/useGenericEffect';
import * as React from 'react';



export const UseSnakeBarModal = (MessageApiAuth: string | null): boolean =>
{
    const [open, setOpen] = React.useState(false);
    const timeOutSnakeBar = () =>
    {
        if (MessageApiAuth != null) 
        {
            setOpen(true);
            const timer = setTimeout(() => {
                setOpen(false);
            }, 5000);
        
            return () => clearTimeout(timer);
        }
    }
    
      useGenericEffect(timeOutSnakeBar, []);

      return open;
};