import Table from "./table.type";

type Reservation = {
	ID: number;
	FirstName: string;
	LastName: string;
	Email: string;
	Date: Date;
	NumberOfPeople: number;
	NumberOfTables: Table;
};

export default Reservation;