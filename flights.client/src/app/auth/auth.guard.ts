import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.service';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {

  const authService = inject(AuthService);
  const router = inject(Router);

  if (!authService.currentUser) {
    return router.parseUrl('/register-passenger'); // better than navigate, since it doesn't attempt double navigations
  }

  return true;
};
