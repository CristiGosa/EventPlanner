import { SocialAuthServiceConfig, GoogleSigninButtonDirective, GoogleLoginProvider } from '@abacritt/angularx-social-login';

const googleLoginConfig = new GoogleLoginProvider(
  '31114608670-r5efmhijvi9c6ucgeareenfgmg5go4io.apps.googleusercontent.com',
  {
    oneTapEnabled: false,
    scopes: 'email'
  }
);

const socialAuthConfig: SocialAuthServiceConfig = {
  autoLogin: false,
  providers: [
    {
      id: GoogleLoginProvider.PROVIDER_ID,
      provider: googleLoginConfig
    }
  ],
  onError: (err) => {
    console.error(err);
  }
}

export const GoogleSigninProvider = [
  {
    provide: 'SocialAuthServiceConfig',
    useValue: socialAuthConfig
  },
  GoogleSigninButtonDirective
];