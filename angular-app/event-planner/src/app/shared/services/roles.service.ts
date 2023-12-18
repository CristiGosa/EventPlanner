import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class RolesService {
  constructor() {}

  isAdmin(): boolean {
    return localStorage.getItem('roles')?.toString().includes('Admin')? true: false;
  }

  isUser(): boolean{
    return  localStorage.getItem('roles')?.toString().includes('User')? true: false;
  }
  
  isOrganizer(): boolean {
    return localStorage.getItem('roles')?.toString().includes('Organizer')? true: false;
  }
}
