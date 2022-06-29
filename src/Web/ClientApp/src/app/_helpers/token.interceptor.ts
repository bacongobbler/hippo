import { Injectable } from '@angular/core';
import {
	HttpRequest,
	HttpHandler,
	HttpEvent,
	HttpInterceptor} from '@angular/common/http';
import { Observable } from 'rxjs';
import { SessionService } from '../_services/session.service';

@Injectable({
	providedIn: 'root'
})
export class TokenInterceptor implements HttpInterceptor {

	constructor(private readonly sessionService: SessionService) { }

	intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
		const token = this.sessionService.tokenValue.token;

		if (token) {
			request = request.clone({
				setHeaders: { Authorization: `Bearer ${token}` }
			});
		}

		return next.handle(request);
	}
}
