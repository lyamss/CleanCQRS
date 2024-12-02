'use client'

import 'tailwindcss/tailwind.css';
import Loader1 from "@/components/Loading/Loader1"
import { GetItemsDto, GetUserDto } from '@/services/modelsDto/Users';
import { useEffect, useState } from 'react';
import { UseUser } from '@/services/UseUser';
import { AuthProvider } from '@/services/AuthProvider';
import { Pagination, Tab, Tabs } from '@mui/material';
import { LoadingPagePrincipale } from '@/components/Dashboard/Page/LoadingPagePrincipale';
import { ItemCard } from '@/components/Dashboard/Page/ItemCard';
import { UserCard } from '@/components/Dashboard/Page/UserCard';
import { usePagination, usePaginationTab } from '@/services/UsePagination';

const ITEMS_PER_PAGE = 6;

const HomePage = () => {
  const { UserAllDto, ItemsAllDto } = UseUser();
  const [UserAllDtoInArray, SetUserAllDtoInArray] = useState<GetUserDto[] | null>(null);
  const [ItemsAllDtoInArray, SetItemsAllDtoInArray] = useState<GetItemsDto[] | null>(null);

  const userPagination = usePagination({ items: UserAllDtoInArray, itemsPerPage: ITEMS_PER_PAGE });
  const itemPagination = usePagination({ items: ItemsAllDtoInArray, itemsPerPage: ITEMS_PER_PAGE });
  const { activeTab, handleChangeTab } = usePaginationTab();

  useEffect(() => {
    if(Array.isArray(UserAllDto)) {
      SetUserAllDtoInArray(UserAllDto);
    }
  }, [UserAllDto]);

  useEffect(() => {
    if(Array.isArray(ItemsAllDto)) {
      SetItemsAllDtoInArray(ItemsAllDto);
    }
  }, [ItemsAllDto]);

  const currentPagination = activeTab === 0 ? userPagination : itemPagination;

  return (
    <AuthProvider LoadingComponent={<Loader1/>} isProtected={true}>
      <div className="min-h-screen bg-gray-100 py-12 px-4 sm:px-6 lg:px-8">
        <div className="max-w-7xl mx-auto">
          <Tabs value={activeTab} onChange={handleChangeTab} centered className="mb-8">
            <Tab label="Users" />
            <Tab label="Items" />
          </Tabs>
          
          {(UserAllDtoInArray && ItemsAllDtoInArray) ? (
            <>
              <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3">
                {currentPagination.currentPageItems?.map((item) => (
                  activeTab === 0 ? (
                    <UserCard key={(item as GetUserDto).id_User} user={item as GetUserDto} />
                  ) : (
                    <ItemCard key={(item as GetItemsDto).id_items} item={item as GetItemsDto} />
                  )
                ))}
              </div>
              <div className="mt-8 flex justify-center">
                <Pagination 
                  count={currentPagination.totalPages} 
                  page={currentPagination.currentPage} 
                  onChange={currentPagination.handleChangePage} 
                  color="primary" 
                />
              </div>
            </>
          ) : (
            <LoadingPagePrincipale />
          )}
        </div>
      </div>
    </AuthProvider>
  );
}

export default HomePage