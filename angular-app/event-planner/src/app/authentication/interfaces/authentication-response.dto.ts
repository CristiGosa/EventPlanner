export interface AuthenticationResponseDto {
  token: string;
  roles: string[];
  refreshToken: string;
}
