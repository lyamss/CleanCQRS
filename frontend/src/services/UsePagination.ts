import { useState, useMemo } from 'react';

interface UsePaginationProps<T> {
  items: T[] | null;
  itemsPerPage: number;
}

export function usePagination<T>({ items, itemsPerPage }: UsePaginationProps<T>) {
  const [currentPage, setCurrentPage] = useState(1);


  const totalPages = useMemo(() => {
    return Math.ceil((items?.length || 0) / itemsPerPage) || 1;
  }, [items, itemsPerPage]);


  const currentPageItems = useMemo(() => {
    if (!items) return null;
    const startIndex = (currentPage - 1) * itemsPerPage;
    const endIndex = startIndex + itemsPerPage;
    return items.slice(startIndex, endIndex);
  }, [items, currentPage, itemsPerPage]);


  const handleChangePage = (event: React.ChangeEvent<unknown>, value: number) => {
    setCurrentPage(value);
  };


  return {
    currentPage,
    totalPages,
    currentPageItems,
    handleChangePage,
  };
}


export function usePaginationTab() 
{
  const [activeTab, setActiveTab] = useState(0);

  const handleChangeTab = (event: React.SyntheticEvent, newValue: number) => {
    setActiveTab(newValue);
  };

  return {activeTab, handleChangeTab}
}
