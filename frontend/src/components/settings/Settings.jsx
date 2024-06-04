import Link from '@mui/material/Link';
import Grid from "@mui/material/Unstable_Grid2";

export default function Settings() 
{
    return (
      <Grid container spacing={2} className="m-2">
        <Grid xs={4}>
            <Link href="/settings/thought-buckets">Thought Buckets</Link>
          </Grid>
      </Grid>
    );
}


