import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import Day from '../../types/day.type';

 @Component({
	selector: 'Calendar',
	standalone: true,
	imports: [FormsModule, ReactiveFormsModule, CommonModule],
	templateUrl: './calendar.component.html',
	styleUrl: './calendar.component.css'
})
export class CalendarComponent {
	@Input() days: Day[] | undefined;
	@Input() offset: number | undefined;
	@Output() onDateSelected = new EventEmitter<Day>();

	selectedDay: Day | null = null;

	constructor() {}

	onDayChange(day: Day): void {
		this.selectedDay = day;
		this.onDateSelected.emit(day);
	}
}