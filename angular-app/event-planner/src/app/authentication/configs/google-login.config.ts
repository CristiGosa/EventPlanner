import { SocialAuthServiceConfig, GoogleSigninButtonDirective, GoogleLoginProvider } from '@abacritt/angularx-social-login';

const googleLoginConfig = new GoogleLoginProvider(
  '743850047685-jm2vc2qs705sbee1aq976d6doq089i5d.apps.googleusercontent.com',
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