import Link from "@mui/material/Link";
import { Grid } from "tabler-react";

export default function Settings() {
  return (
    <Grid container>
      <Grid.Row className="m-2">
        <Grid.Col md={4} width={12}>
          <Link href="/settings/thought-buckets">Thought Buckets</Link>
        </Grid.Col>
      </Grid.Row>
      <Grid.Row className="m-2">
        <Grid.Col md={4} width={12}>
          <Link href="/settings/thought-buckets">Thought Detail Templates</Link> 
        </Grid.Col>
      </Grid.Row>
    </Grid>
  );
}
