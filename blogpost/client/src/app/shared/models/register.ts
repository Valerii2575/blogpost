export interface RegisterCommand{
  RegisterDto: RegisterDto;
}

export interface RegisterCommandResult{
  message: string;
  status: number;
}

export interface RegisterDto{
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}
