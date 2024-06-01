import Table from "./table.type";

type Reservation = {
	id: number;
	firstName: string;
	lastName: string;
	email: string;
	date: Date;
	numberOfPeople: number;
	table: number;
};

export default Reservation;