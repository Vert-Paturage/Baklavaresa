import { Injectable } from '@angular/core';
import {SnackbarComponent} from "../components/snackbar/snackbar.component";
import {MatSnackBar} from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {

  constructor(private snackBar: MatSnackBar) { }

  showSnackbar(content: string, action: string) {
    let sb = this.snackBar.open(content, action, {
      duration: 4000,
      panelClass: ['custom-style'],
      verticalPosition: 'top',
      horizontalPosition: 'right',
    });
    sb.onAction().subscribe(() => {
      sb.dismiss();
    });
  }
}