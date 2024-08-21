import { ChangeDetectionStrategy, ChangeDetectorRef, Component, inject, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, Validators } from '@angular/forms';

interface Conditions {
	conditions: string;
	reasons: string;
}

@Component({
	selector: 'app-home',
	templateUrl: './home.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomeComponent {
	private http = inject(HttpClient);
	private fb = inject(FormBuilder);
	private cdr = inject(ChangeDetectorRef);

	public response: Conditions | undefined;

	form = this.fb.group({
		date: ['2024-08-01', [Validators.required]],
		longitude: [-73.561668, [Validators.required]],
		latitude: [45.508888, [Validators.required]],
	});

	constructor(@Inject('BASE_URL') private baseUrl: string) {
	}

	public submit() {
		if (!this.form.valid) {
			alert("form is not valid");
			return;
		}

		const value = this.form.value;
		this.http.get<Conditions>(this.baseUrl + `api/frisbee?date=${value.date}&longitude=${value.longitude}&latitude=${value.latitude}`)
			.subscribe({
				next: r => {
					this.response = r;
					this.cdr.markForCheck();
				},
				error: error => console.error(error)
			});
	}
}
