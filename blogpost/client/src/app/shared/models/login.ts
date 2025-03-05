import { UserDto } from "./userDto";

export interface LoginCommand{
  LoginDto: LoginDto;
}

export interface LoginCommandResult{
  user: UserDto;
  message: string;
  status: number;
}

export interface LoginDto{
  email: string;
  password: string;
}


export interface GetRefreshUserTokenQueryResult{
  userDto: UserDto;
}
