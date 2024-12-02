import { GetItemsDto } from "@/services/modelsDto/Users";
import { CalendarIcon, ShoppingBagIcon } from "lucide-react";

export const ItemCard = ({ item }: { item: GetItemsDto }) => (
    <div key={item.id_items} className="bg-white overflow-hidden shadow rounded-lg">
      <div className="px-4 py-5 sm:p-6">
        <div className="flex items-center">
          <ShoppingBagIcon className="h-10 w-10 text-gray-400" />
          <div className="ml-5 w-0 flex-1">
            <dl>
              <dt className="text-sm font-medium text-gray-500 truncate">Item Name</dt>
              <dd className="text-lg font-bold text-gray-900">{item.name}</dd>
            </dl>
          </div>
        </div>
        <div className="mt-5">
          <p className="text-sm text-gray-500">{item.description}</p>
          <div className="mt-2 flex items-center">
            <span className="text-sm font-medium text-gray-900">${item.price.toFixed(2)}</span>
          </div>
          <div className="mt-2 flex items-center">
            <CalendarIcon className="h-5 w-5 text-gray-400" />
            <span className="ml-2 text-sm text-gray-500">
              {new Date(item.createdAt).toLocaleDateString()}
            </span>
          </div>
        </div>
      </div>
    </div>
  );