import { Component } from '@angular/core';
import { AuthServiceService } from './auth-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: []
})
export class AppComponent {
  title = 'ClientApp';
  displayedMessage: string = "";

  constructor(private _authService: AuthServiceService) { }

  async button0_click() {
    this.displayedMessage = await this._authService.getToken();
  }

  async button1_click() {
    this.displayedMessage = await this._authService.getAdminOnly();
  }

  async button2_click() {
    this.displayedMessage = await this._authService.getAdminOrManager();
  }

  async button3_click() {
    this.displayedMessage = await this._authService.getAuthenticatedUser();
  }

  async button4_click() {
    this.displayedMessage = await this._authService.getMinimumAgeUser();
  }
}
