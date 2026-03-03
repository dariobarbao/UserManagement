import { User } from './user.model';

export interface UsersResponse {
  users: User[];
}

export interface UserResponse {
  user: User;
}
