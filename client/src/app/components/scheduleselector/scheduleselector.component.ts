import { Component, EventEmitter, Input, Output } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";

import { getDayName, getMonthName } from "../../utils/dateStringHandler";

@Component({
	selector: "ScheduleSelector",
	standalone: true,
	imports: [FormsModule, ReactiveFormsModule, CommonModule],
	templateUrl: "./scheduleselector.component.html",
	styleUrl: "./scheduleselector.component.css"
})
export class ScheduleSelectorComponent {
	@Input() schedules: Date[] | undefined;

	@Output() onScheduleSelected = new EventEmitter<Date>();

	selectedSchedule: Date | null = null;

	constructor() {}

	onScheduleChange(schedule: Date) {
		this.selectedSchedule = schedule;
		this.onScheduleSelected.emit(schedule);
	}

	getDateString(): string {
		return getDayName(this.schedules![0].getDay() ?? 0)
			+ " "
			+ this.schedules![0].getDate()
			+ " "
			+ getMonthName(this.schedules![0].getMonth() ?? 0)
			+ " "
			+ this.schedules![0].getFullYear();
	}
}