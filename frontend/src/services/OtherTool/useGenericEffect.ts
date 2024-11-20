import { useEffect, useCallback } from 'react';

type CallbackFunction = () => void;


export const useGenericEffect = (callback: CallbackFunction, dependencies: any[]) => {
    const memoizedCallback = useCallback(callback, dependencies);
  
    useEffect(() => {
      memoizedCallback();
    }, [memoizedCallback]);
  };