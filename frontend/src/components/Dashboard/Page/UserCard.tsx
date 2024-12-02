import { GetUserDto } from "@/services/modelsDto/Users";
import { UserIcon, CalendarIcon, AtSignIcon as AtSymbolIcon } from 'lucide-react'


export const UserCard = ({ user }: { user: GetUserDto }) => (
    <div key={user.id_User} className="bg-white overflow-hidden shadow rounded-lg">
      <div className="px-4 py-5 sm:p-6">
        <div className="flex items-center">
          <UserIcon className="h-10 w-10 text-gray-400" />
          <div className="ml-5 w-0 flex-1">
            <dl>
              <dt className="text-sm font-medium text-gray-500 truncate">User ID</dt>
              <dd className="text-lg font-bold text-gray-900">{user.id_User}</dd>
            </dl>
          </div>
        </div>
        <div className="mt-5">
          <div className="flex items-center">
            <AtSymbolIcon className="h-5 w-5 text-gray-400" />
            <span className="ml-2 text-sm text-gray-500">{user.email}</span>
          </div>
          <div className="mt-2 flex items-center">
            <CalendarIcon className="h-5 w-5 text-gray-400" />
            <span className="ml-2 text-sm text-gray-500">
              {new Date(user.accountCreatedAt).toLocaleDateString()}
            </span>
          </div>
        </div>
      </div>
    </div>
  );